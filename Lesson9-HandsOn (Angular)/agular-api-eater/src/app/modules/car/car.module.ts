import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import get cars component
import { CarComponent } from 'src/app/modules/car/components/car/car.component';
//import the formsModule
import { FormsModule } from '@angular/forms';
//import the httpclientmodule
import { HttpClientModule } from '@angular/common/http';
import { ModifyCarsComponent } from './components/modify-cars/modify-cars.component';


@NgModule({
  declarations: [
    CarComponent,
    ModifyCarsComponent
  ],
  imports: [
    CommonModule, HttpClientModule, FormsModule
  ],
  exports: [
    CarComponent
  ]
})
export class CarModule { }
