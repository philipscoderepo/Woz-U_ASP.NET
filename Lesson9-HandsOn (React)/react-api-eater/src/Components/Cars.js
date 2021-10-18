import React from "react";
import { render } from "react-dom";
import '../App.css'

class Car{
    constructor(Id, Make, Model, Year) {
        this.Id = Id;
        this.Make = Make;
        this.Model = Model;
        this.Year = Year;
    }
}

class Cars extends React.Component{
    state = {
        cars: []
    }
    
    callCars = async () => {
        console.log("Fetching cars");
        //Get the current cars in the DB
        await fetch('http://localhost:5000/api/Car', 
        {method: 'GET', mode: 'cors', 
        headers: {'Access-Control-Allow-Origin': '*', 'Content-Type': 'application/json', 'Accept': 'application/json'}})
        .then(response => response.json())
        .then(data => {
            console.log(data);
            //create a temp list of cars
            let cars = [];
            data.forEach(c => {
                let newCar = new Car(c.id, c.make, c.model, c.year);
                cars.push(newCar);
            });
            console.log("Cars Array: ", cars);
            //update the state
            this.setState({
                cars: cars
            });
            console.log("State set")
        });
        
        return;
    };

    async componentDidMount(){
        await this.callCars();
    }

    undoLastDelete = async () => {
        //Get the last deleted car
        await fetch('http://localhost:5000/api/LastDeleted', 
        {method: 'GET', mode: 'cors', 
        headers: {'Access-Control-Allow-Origin': '*', 'Content-Type': 'application/json', 'Accept': 'application/json'}})
        .then(response => response.json())
        .then(data => {
            console.log("Last deleted: ", data);
            //if data is not found, there is nothing to undo
            if(data.status === 404){
                alert("Nothing to undo");
            }else{
                //add the car to the current list of cars
                let cars = this.state.cars;
                //add it to the end and give it 1 + the length
                //to avoid id conflicts
                let newCar = new Car(this.state.cars.length+1, data.make, data.model, data.year);
                cars.push(newCar);
                console.log("Cars Array: ", cars);
                this.setState({
                    cars: cars
                });
                console.log("State set");
            }
        });
    }

    handleDelete = async (id) =>{
        //store the deleted item in a seperate table
        let c = this.state.cars[id-1];
        let newCar = new Car(1, c.Make, c.Model, c.Year);
        console.log("Modify car: ", newCar);
        //request to PUT at the selected id
        fetch('http://localhost:5000/api/LastDeleted', {
            method: 'PUT', 
            headers: {"Access-Control-Allow-Origin" : "*", 'Content-Type': 'application/json'}, 
            body: JSON.stringify(newCar)})
        .then(r => r.json())
        .then(res => {
            if(res){
                console.log("PUT successful");
                return;
            }
        });

        //send a delete request for id and update view
        await fetch('http://localhost:5000/api/Car/' + id, {method: 'DELETE'});
        console.log("DELETE successful");
        window.location.reload();
    }

    handleCancel = () =>{
        this.onDelete(-1, false);
    }
    
    onDelete = (id, isOpen) =>{
        //display a pop up asking if the user wants to delete
        //html
        const deleteRequest = (
            <div>
                {isOpen && 
                <div className={"popup-box"}>
                    <div className={"box"}>
                        <p>Are you sure you want to delete this entry?</p>
                        <button onClick={() => this.handleDelete(id)}>Delete</button>
                        <button onClick={() => this.handleCancel(0, false)}>Cancel</button>
                    </div>
                </div>
                }
            </div>
        )
        //renders a popup asking if they want to delete the node
        render(
            deleteRequest,
            document.getElementById("delete")
        )
    }

    onAdd = async () => {
        //send a POST request and update view
        const make = document.getElementById("add_make_input").value;
        const model = document.getElementById("add_model_input").value;
        const year = document.getElementById("add_year_input").value;
        console.log(make + " " + model + " " + year);
        if(make != null && model != null && year != null){
            if(year > 1885){
                let newCar = new Car(this.state.cars.length + 1, make, model, year);
                console.log("Add car: ", newCar);
                await fetch('http://localhost:5000/api/Car', {
                    method: 'POST', 
                    headers: {'Content-Type': 'application/json'}, 
                    body: JSON.stringify(newCar)})
                .then(r => r.json())
                .then(res => {
                    if(res){
                        console.log("POST successful");
                        return;
                    }
                })
            }
        }
    }

    onModify = async () =>{
        //send a PUT request and update view 
        //Get the value of the select index of the modify select drop down
        const select = document.getElementById("modify_select");
        const id = select.options[select.selectedIndex].value;
        console.log("Modifying " + id);
        //Get the values from the form
        const make = document.getElementById("modify_make_input").value;
        const model = document.getElementById("modify_model_input").value;
        const year = document.getElementById("modify_year_input").value;
        console.log(id + ": " + make + " " + model + " " + year);
        //Check the data
        if(make != null && model != null && year != null){
            if(year > 1885){
                let newCar = new Car(id, make, model, year);
                console.log("Modify car: ", newCar);
                //request to PUT at the selected id
                await fetch('http://localhost:5000/api/Car/'+ id, {
                    method: 'PUT', 
                    headers: {"Access-Control-Allow-Origin" : "*", 'Content-Type': 'application/json'}, 
                    body: JSON.stringify(newCar)})
                .then(r => r.json())
                .then(res => {
                    if(res){
                        console.log("PUT successful");
                        return;
                    }
                })
            }
        } 
    }

    render(){
        return(
            <div>
                <div className={"form-container"}>
                    <form>
                        <button onClick={() => this.onAdd()}>Add New Car</button>
                        <br/>
                        <label htmlFor={"make"}>Make</label>
                        <input name={"make"} id={"add_make_input"}></input>
                        <br/>
                        <label htmlFor={"model"}>Model</label>
                        <input name={"model"} id={"add_model_input"}></input>
                        <br/>
                        <label htmlFor={"year"}>Year</label>
                        <input type={"number"} name={"year"} id={"add_year_input"}></input>
                    </form>
                </div>
                <div className={"form-container"}>
                    <form>
                        <button onClick={() => this.onModify()}>Modify Existing Car</button>
                        <select id={"modify_select"}>
                        {this.state.cars.map((car)=>(
                            <option key={car.Id} value={car.Id}>{car.Id}</option>
                        ))}
                        </select>
                        <br/>
                        <label htmlFor={"make"}>Make</label>
                        <input name={"make"} id={"modify_make_input"}></input>
                        <br/>
                        <label htmlFor={"model"}>Model</label>
                        <input name={"model"} id={"modify_model_input"}></input>
                        <br/>
                        <label htmlFor={"year"}>Year</label>
                        <input type={"number"}name={"year"} id={"modify_year_input"}></input>
                    </form>
                </div>
                <div className={"cars-container"}>
                    {this.state.cars.map((car)=>(
                        <div key={car.Id} id={"car_"+car.Id}>
                            <div className={"car"}>{car.Id}. {car.Make} {car.Model}: {car.Year} 
                                <button className={"delete-button"}onClick={() => this.onDelete(car.Id, true)}>Delete</button>
                            </div>
                            <br/>
                        </div>
                    ))}
                    <button className={"undo-button"} onClick={() => this.undoLastDelete()}>Undo Last Delete</button>
                    <div id={"delete"}></div>
                </div>
            </div>
        );   
    }
}

export default Cars;