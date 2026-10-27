import { Component, input, output } from '@angular/core';
import { EstadoProductoPipe } from '../estado-producto-pipe';
import { Producto } from '../models/producto.model';
import { ResaltarDestacado } from '../resaltar-destacado';

@Component({
  imports: [EstadoProductoPipe, ResaltarDestacado],
  selector: 'app-producto-card',
  styleUrl: './producto-card.css',
  templateUrl: './producto-card.html',
})
export class ProductoCard {
  producto = input.required<Producto>();
  verDetalle = output<number>();
}
