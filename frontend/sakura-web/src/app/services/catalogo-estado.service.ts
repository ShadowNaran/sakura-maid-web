import { Injectable, signal } from '@angular/core';
import { Categoria } from '../models/categoria.model';
import { Producto } from '../models/producto.model';

@Injectable({ providedIn: 'root' })
export class CatalogoEstadoService {
  private readonly _productos = signal<Producto[]>([]);
  private readonly _categorias = signal<Categoria[]>([]);

  readonly productos = this._productos.asReadonly();
  readonly categorias = this._categorias.asReadonly();

  establecerProductos(productos: Producto[]): void {
    this._productos.set(productos);
  }

  establecerCategorias(categorias: Categoria[]): void {
    this._categorias.set(categorias);
  }
}
