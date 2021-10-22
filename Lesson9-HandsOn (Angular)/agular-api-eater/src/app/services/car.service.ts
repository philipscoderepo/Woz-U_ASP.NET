import { Injectable } from '@angular/core';
//import
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Car } from 'src/app/shared/models/car';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private cars: Array<Car> = new Array<Car>();
  private taskRoute = 'http://localhost:5000/api/car';
  //used for http
  httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
  }

  constructor() { }

  async getTasks(): Promise<Array<Car>>{
    await fetch(this.taskRoute)
    .then(resp => resp.json())
    .then(resp => {
      console.log("api response: ", resp);
      this.cars = resp;
      console.log("this.cars: ", this.cars);
    });
    
    return this.cars;
  }
}
