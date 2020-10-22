import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {


    eventos: any;
/*   eventos: any = [
    {
      EventoId:1,
      Tema:"Angular",
      Local:"Sorocaba"
    },
    {
      EventoId:2,
      Tema:"Warlocks",
      Local:"Orgrimmar"
    }
  ] */

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){
    this.http.get("http://localhost:5000/api/values").subscribe(
        response => 
          {
            this.eventos = response;
          },
          error => {
            console.log(error);
          }
      );
  }

}
