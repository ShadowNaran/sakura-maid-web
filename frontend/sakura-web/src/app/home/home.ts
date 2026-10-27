import { Component, computed, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CAFE_CONFIG } from '../core/config/cafe.config';
import { ProductoCard } from '../producto-card/producto-card';
import { CatalogoEstadoService } from '../services/catalogo-estado.service';
import { CategoriaService } from '../services/categoria.service';
import { ProductoService } from '../services/producto.service';

@Component({
  imports: [RouterLink, ProductoCard],
  selector: 'app-home',
  styleUrl: './home.css',
  templateUrl: './home.html',
})
export class Home {
  private readonly productoService = inject(ProductoService);
  private readonly categoriaService = inject(CategoriaService);
  private readonly catalogoEstado = inject(CatalogoEstadoService);
  private readonly router = inject(Router);

  productos = this.catalogoEstado.productos;
  categorias = this.catalogoEstado.categorias;
  error = signal('');
  readonly cafe = CAFE_CONFIG;

  productosDestacados = computed(() => {
    const destacados = this.productos().filter((producto) => producto.esDestacado);
    return (destacados.length > 0 ? destacados : this.productos()).slice(0, 3);
  });

  categoriasDestacadas = computed(() => this.categorias().slice(0, 3));

  constructor() {
    this.productoService.obtenerProductos().subscribe({
      next: (productos) => this.catalogoEstado.establecerProductos(productos),
      error: () => this.error.set('No se pudieron cargar los productos.'),
    });

    this.categoriaService.obtenerCategorias().subscribe({
      next: (categorias) => this.catalogoEstado.establecerCategorias(categorias),
      error: () => this.error.set('No se pudieron cargar las categorías.'),
    });
  }

  abrirDetalle(id: number): void {
    this.router.navigate(['/productos', id]);
  }
}
