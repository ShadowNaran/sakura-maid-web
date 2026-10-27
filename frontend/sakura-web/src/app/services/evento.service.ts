import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../core/config/api.config';
import { Evento, EventoRequest } from '../models/evento.model';

@Injectable({ providedIn: 'root' })
export class EventoService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_CONFIG.baseURL}/eventos`;

  obtenerEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.url);
  }

  crearEvento(evento: EventoRequest): Observable<Evento> {
    return this.http.post<Evento>(this.url, evento);
  }

  actualizarEvento(id: number, evento: EventoRequest): Observable<Evento> {
    return this.http.patch<Evento>(`${this.url}/${id}`, evento);
  }

  eliminarEvento(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.url}/${id}`);
  }
}
