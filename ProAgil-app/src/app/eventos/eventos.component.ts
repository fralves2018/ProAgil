import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
//import { get } from 'http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

    eventosFiltrados: Evento[];
    eventos: Evento[];
    imagemLargura: number = 50;
    imagemMargem: number = 2;
    mostrarImagem: boolean = false;
    modalRef: BsModalRef;
    _filtroLista: string;

    constructor(
      private eventoService: EventoService,
      private modalService: BsModalService
      ) { }

    get filtroLista(): string {
      return this._filtroLista;
    }
    set filtroLista(value: string)
    {
      this._filtroLista = value;
      this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventosFiltrados;
    }

    openModal(template: TemplateRef<any>) {
      this.modalRef = this.modalService.show(template);
    }

  ngOnInit() {
    this.getEventos();
  }

  //Essa função recebe um valor string na variavel "filtrarPor". 
  filtrarEventos(filtrarPor: string): Evento[] {
    //transforma a variavel recebida em letras minusculas
    filtrarPor = filtrarPor.toLocaleLowerCase();
    //Aqui ela vai usar um filtro do angular para procurar na lista de eventos criada na
    //função "getEventos" abaixo o termo dentro da variável "filtrarPor";
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe(
        (_eventos: Evento[]) => 
          {
            this.eventos = _eventos;
            this.eventosFiltrados = this.eventos;
            console.log(_eventos);
          },
          error => {
            console.log(error);
          }
      );
  }

}
