import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Eventos } from '../_models/Eventos';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  
  eventos: Object;
  baseURL = 'http://localhost:5000/eventos';

  constructor(private http: HttpClient) { }

  getAllEvento():Observable<Eventos[]> {    
    return this.http.get<Eventos[]>(this.baseURL);
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

  postUpload(file: File) {

    const fileToUpload = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    return this.http.post(`${this.baseURL}/upload/`, formData);
  }
  
}
