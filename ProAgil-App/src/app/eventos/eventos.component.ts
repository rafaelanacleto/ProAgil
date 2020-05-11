import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [];
  eventosFiltrados: any = [];
  mostrarImagem: boolean = false;

  _filtroList: string;

  get filtroLista(): string {
    return this._filtroList;
  }

  set filtroLista(value: string) {
    this._filtroList = value;
    this.eventosFiltrados = this._filtroList ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEvento(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );

  }

  alternarImagem () {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.http.get('http://localhost:5000/eventos').subscribe(
      response =>
        {
          this.eventos = response;
          console.log(response);
        }, error => { console.log(error);}
    );
  }
}
