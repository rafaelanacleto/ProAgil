import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Eventos } from '../_models/Eventos';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { templateJitUrl } from '@angular/compiler';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: Eventos[];
  evento: Eventos;
  eventosFiltrados: Eventos[];
  mostrarImagem: boolean = false;
  _filtroList: string;
  registerForm: FormGroup;
  modalType: string;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private local: BsLocaleService
  ) {
    this.local.use('pt-br');
   }

  get filtroLista(): string {
    return this._filtroList;
  }

  set filtroLista(value: string) {
    this._filtroList = value;
    this.eventosFiltrados = this._filtroList ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  editaTelaEvento(evento: Eventos, template) {

    template.show();
    this.modalType = "edit";
    this.registerForm.patchValue({
      tema: evento.tema,
      local: evento.local,
      dataEvento: evento.dataEvento,
      qtdPessoas: evento.qtdPessoas,
      imagemUrl: evento.imagemUrl,
      telefone: evento.telefone,
      email: evento.email
    });
  }

  deleteEvento(idevento: number, template: any) {

    if (window.confirm("Você realmente deseja excluir Evento? ")) { 
      this.eventoService.deleteEvento(idevento).subscribe(
        (retorno: Eventos) => {
          console.log(retorno);
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
      );
    }
    
  }

  salvarAlteracao(template: any) {
    
    if(this.registerForm.valid)
    {
      if(this.modalType == "edit")
      {
        this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
        this.eventoService.putEvento(this.evento.id, template).subscribe(
          (retorno: Eventos) => {
            console.log(retorno);
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );

        this.modalType = "";
      }

      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Eventos) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        } 
      ); 
    }

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
