import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyCarsComponent } from './modify-cars.component';

describe('ModifyCarsComponent', () => {
  let component: ModifyCarsComponent;
  let fixture: ComponentFixture<ModifyCarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModifyCarsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
