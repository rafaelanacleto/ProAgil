import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Eventos } from '../_models/Eventos';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: Eventos[];
  eventosFiltrados: Eventos[];
  mostrarImagem: boolean = false;

  _filtroList: string;

  get filtroLista(): string {
    return this._filtroList;
  }

  set filtroLista(value: string) {
    this._filtroList = value;
    this.eventosFiltrados = this._filtroList ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  constructor(private eventoService: EventoService) { }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEvento(filtrarPor: string): Eventos[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem () {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (_eventos: Eventos[]) =>
        {
          this.eventos = _eventos;          
          this.eventosFiltrados = this.eventos;
          console.log(_eventos);
        }, error => { console.log(error);}
    );
  }
}
