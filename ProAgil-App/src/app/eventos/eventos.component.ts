import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Eventos } from '../_models/Eventos';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

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
  modalRef: BsModalRef;
  registerForm: FormGroup;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder
  ) { }

  get filtroLista(): string {
    return this._filtroList;
  }

  set filtroLista(value: string) {
    this._filtroList = value;
    this.eventosFiltrados = this._filtroList ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  salvarAlteracao() {
    console.log("metodo para salvar o form");
  }

  validation() {
    this.registerForm = new FormGroup({
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(25)]),
      local: new FormControl('', Validators.required),
      dataEvento: new FormControl('', Validators.required),
      qtdPessoas: new FormControl('', Validators.required),
      imagemUrl: new FormControl('', Validators.required),
      telefone: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  ngOnInit() {
    this.validation();
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
