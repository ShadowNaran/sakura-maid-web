import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../core/config/api.config';
import { Novedad, NovedadRequest } from '../models/novedad.model';

@Injectable({ providedIn: 'root' })
export class NovedadService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_CONFIG.baseURL}/novedades`;

  obtenerNovedades(): Observable<Novedad[]> {
    return this.http.get<Novedad[]>(this.url);
  }

  crearNovedad(novedad: NovedadRequest): Observable<Novedad> {
    return this.http.post<Novedad>(this.url, novedad);
  }

  actualizarNovedad(id: number, novedad: NovedadRequest): Observable<Novedad> {
    return this.http.patch<Novedad>(`${this.url}/${id}`, novedad);
  }

  eliminarNovedad(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.url}/${id}`);
  }
}
