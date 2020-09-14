import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Eventos } from '../_models/Eventos';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  
  eventos: Object;
  baseURL = 'http://localhost:5000/eventos';
  tokenHeader: HttpHeaders;

  constructor(private http: HttpClient) { 
    this.tokenHeader = new HttpHeaders({ 'Authorization': `Bearer ${localStorage.getItem('token')}`});
  }

  getAllEvento():Observable<Eventos[]> {    
    return this.http.get<Eventos[]>(this.baseURL, {headers: this.tokenHeader });
  }

  getEventoByTema(tema: string):Observable<Eventos[]> {    
    return this.http.get<Eventos[]>(`${this.baseURL}/getByTema/${tema}`, {headers: this.tokenHeader });
  }

  getEventoById(id: number):Observable<Eventos> {    
    return this.http.get<Eventos>(`${this.baseURL}/${id}`, {headers: this.tokenHeader });
  }

  postEvento(evento: Eventos) {    
    return this.http.post(this.baseURL, evento, {headers: this.tokenHeader } );
  }

  putEvento(evento: Eventos) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento, {headers: this.tokenHeader });
  }

  deleteEvento(idEvento: number) {
    return this.http.delete(`${this.baseURL}/${idEvento}`, {headers: this.tokenHeader });
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);

    return this.http.post(`${this.baseURL}/upload`, formData, {headers: this.tokenHeader });
  } 
  
}
