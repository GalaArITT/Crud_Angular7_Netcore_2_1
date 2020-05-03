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
  InsertarAlumno(alumno){
    return this.http.post(url + 'Post', alumno)
  }
  Eliminar(alumno){
    return this.http.delete(url + 'EliminarAlumno', {params:{'id':alumno} });
  }
  EditarAl(id, alumno){
    let zelda = `${url}Put/${id}/`;
    return this.http.put( zelda, alumno )
  }
}
