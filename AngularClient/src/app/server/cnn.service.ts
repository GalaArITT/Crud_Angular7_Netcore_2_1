import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const url = "https://localhost:44393/api/Alumno/" //conecction 
@Injectable({
  providedIn: 'root'
})
export class CnnService {

  constructor(private http: HttpClient) { }

  verAlumnos(){
    return this.http.get(url + 'Get');
  }
}
