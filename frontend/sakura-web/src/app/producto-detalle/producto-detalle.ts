import { Component, effect, inject, input, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Producto } from '../models/producto.model';
import { ProductoService } from '../services/producto.service';

@Component({
  imports: [RouterLink],
  selector: 'app-producto-detalle',
  styleUrl: './producto-detalle.css',
  templateUrl: './producto-detalle.html',
})
export class ProductoDetalle {
  id = input.required<string>();
  producto = signal<Producto | null>(null);
  cargando = signal(true);
  error = signal('');

  private readonly productoService = inject(ProductoService);

  constructor() {
    effect(() => {
      const productoId = Number(this.id());
      if (!Number.isInteger(productoId) || productoId <= 0) {
        this.error.set('El identificador del producto no es válido.');
        this.cargando.set(false);
        return;
      }

      this.productoService.obtenerProductoPorId(productoId).subscribe({
        next: (producto) => {
          this.producto.set(producto);
          this.cargando.set(false);
        },
        error: () => {
          this.error.set('No se pudo cargar el producto.');
          this.cargando.set(false);
        },
      });
    });
  }
}
