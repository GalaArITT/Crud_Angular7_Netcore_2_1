import { Component, OnInit } from '@angular/core';
import {CnnService} from '../server/cnn.service'
import {MessageService, ConfirmationService} from 'primeng/api';

import {FormControl,FormGroup,Validators} from '@angular/forms';
import * as signalR from '@aspnet/signalr';


@Component({
  selector: 'app-alumno',
  templateUrl: './alumno.component.html',
  styleUrls: ['./alumno.component.css'],
  providers: [MessageService,ConfirmationService]
})
export class AlumnoComponent implements OnInit {

  //Hub
  private _hubConnection:signalR.HubConnection;
  zeldaHub = "https://localhost:44393/hubAlertas";
  //

  AlumnosFrom: FormGroup;
  msgs:any[]=[];

  //variable 
  ListaAlumno: any;
  btnGuardar:boolean = false;
  btnEditar:boolean = false;

  edit_id;

  constructor(private cnn: CnnService, private messageService: MessageService,
    private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.ConnectionHub();
    this.verAlumno();
    this.FromInit();
  }
  FromInit(){
    this.AlumnosFrom = new FormGroup({
      id: new FormControl(0),
      Nombre: new FormControl(''),
      ApellidoPaterno: new FormControl(''),
      ApellidoMaterno: new FormControl(''),
      Carrera: new FormControl('')
    });
  }
  Limpiar(){
    this.AlumnosFrom.reset({
      id:0,
      Nombre:'',
      ApellidoPaterno:'',
      ApellidoMaterno:'',
      Carrera:''
    });
    this.btnEditar =false;
    this.btnGuardar =true;
  }
  verAlumno(){
    this.cnn.verAlumnos().subscribe(res=>{
      this.ListaAlumno = res;
      console.log(res);
    });    
  }
  Guardar(){
    let guardado_alumnos = Object.assign({},this.AlumnosFrom.value)
    //console.log(guardado_alumnos);
    this.cnn.InsertarAlumno(guardado_alumnos).subscribe(res=>{
      console.log(res);
      if(res){
        this.verAlumno();
        this.messageService.add({severity:'success', summary: 'Insertado', detail:'Order submitted'});
      }
      else{
        this.messageService.add({severity:'error', summary: 'Error Message', detail:'Validation failed'});
      }
    })
  }
  ConfirmarEliminar(id,nom,apP,apM) {
    this.confirmationService.confirm({
        message: '¿Estas seguro de eliminar a'+' '+nom+' '+apP+' '+apM +' ?',
        header: 'Eliminar',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.msgs = [{severity:'info', summary:'Confirmed', detail:'You have accepted'}];
            this.Eliminar(id);
        },
        reject: () => {
            this.msgs = [{severity:'info', summary:'Rejected', detail:'You have rejected'}];
        }
    });
  }
  Eliminar(id){
    this.cnn.Eliminar(id).subscribe(res=> {
      console.log(res);
      // if(res){
      //   this.verAlumno();
      //   this.messageService.add({severity:'success', summary: 'Registro Eliminado', detail:'Order submitted'});
      // }
      // else{
      //   this.messageService.add({severity:'error', summary: 'Error Message', detail:'Validation failed'});
      // }
    });
  }
  
  verEditar(id,nom,apP,apM,carr){
    this.edit_id = id;
    this.AlumnosFrom.setValue({
      id:id,
      Nombre:nom,
      ApellidoPaterno:apP,
      ApellidoMaterno:apM,
      Carrera:carr
    });
    this.btnEditar =true;
    this.btnGuardar =false;
  }
  Editar(){
    this.edit_id;
    console.log(this.edit_id);
    let guardado_alumnos = Object.assign({},this.AlumnosFrom.value)
    this.cnn.EditarAl(this.edit_id,guardado_alumnos).subscribe(res=>{
       console.log(res);
       if(res){
        this.verAlumno();
        // this.messageService.add({severity:'success', summary: 'Registro Eliminado', detail:'Order submitted'});
      }
      else{
        this.messageService.add({severity:'error', summary: 'Error Message', detail:'Validation failed'});
      }
    });
  }

  //signalR

  ConnectionHub(){
    this._hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(this.zeldaHub)
    .build();
    this._hubConnection.on("InsertAlumn",(parametro) => {
      this.verAlumno();
      this.messageService.add({severity:'success',summary:'Insertado Alumno: '+ parametro.nombre ,detail:'Order submited'});

    });

    this._hubConnection.on("EliminarAlumn",() => {
      this.verAlumno();
      this.messageService.add({severity:'success',summary:'Eliminar',detail:'Order submited'});

    });


    this._hubConnection.on("ActualizarAlumn",(parametro) => {
      this.verAlumno();
      this.messageService.add({severity:'success',summary:'Actualizado id:' + parametro,detail:'Order submited'});

    });


    this._hubConnection
    .start()
    .then(() => console.log("Hub Funcionando!"))
    .catch(()=> console.log("Hub no funcionando :(")) 
  }

}
