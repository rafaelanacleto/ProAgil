import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  
  eventos: Object;
  baseURL = 'http://localhost:5000/eventos';

  constructor(private http: HttpClient) { }

  getEvento() {
    
    return this.http.get(this.baseURL);

  }

}
