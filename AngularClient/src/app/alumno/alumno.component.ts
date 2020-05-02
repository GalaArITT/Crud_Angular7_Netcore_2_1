import { Component, OnInit } from '@angular/core';
import {CnnService} from '../server/cnn.service'

@Component({
  selector: 'app-alumno',
  templateUrl: './alumno.component.html',
  styleUrls: ['./alumno.component.css']
})
export class AlumnoComponent implements OnInit {

  constructor(private cnn: CnnService) { }

  ngOnInit() {
    this.verAlumno();
  }
  verAlumno(){
    this.cnn.verAlumnos().subscribe(res=>{
      console.log(res)
    });    
  }

}
