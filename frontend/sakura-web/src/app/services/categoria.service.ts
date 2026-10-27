import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_CONFIG } from '../core/config/api.config';
import { Categoria, CategoriaRequest } from '../models/categoria.model';

@Injectable({ providedIn: 'root' })
export class CategoriaService {
  private readonly http = inject(HttpClient);
  private readonly url = `${API_CONFIG.baseURL}/categorias`;

  obtenerCategorias(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(this.url);
  }

  crearCategoria(categoria: CategoriaRequest): Observable<Categoria> {
    return this.http.post<Categoria>(this.url, categoria);
  }

  actualizarCategoria(id: number, categoria: CategoriaRequest): Observable<Categoria> {
    return this.http.patch<Categoria>(`${this.url}/${id}`, categoria);
  }

  eliminarCategoria(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.url}/${id}`);
  }
}
