import { Component, OnInit } from '@angular/core';
//import
import { Car } from 'src/app/shared/models/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {
  public cars: Array<Car> = new Array<Car>();
  private carService: CarService = new CarService;

  constructor() { }

  ngOnInit(){
    this.carService.getTasks()
    .then(resp => {
      this.cars = resp
    });
  }

}
