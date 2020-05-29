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
  
}