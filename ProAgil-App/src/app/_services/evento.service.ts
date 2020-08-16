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
    return this.http.get<Eventos[]>(`${this.baseURL}/getByTema/${tema}`);
  }

  getEventoById(id: number):Observable<Eventos> {    
    return this.http.get<Eventos>(`${this.baseURL}/${id}`);
  }

  postEvento(evento: Eventos) {    
    return this.http.post(this.baseURL, evento );
  }

  putEvento(evento: Eventos) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }

  deleteEvento(idEvento: number) {
    return this.http.delete(`${this.baseURL}/${idEvento}`);
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);

    return this.http.post(`${this.baseURL}/upload`, formData);
  } 
  
}
