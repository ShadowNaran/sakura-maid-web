import { Component, computed, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Categoria } from '../models/categoria.model';
import { ProductoCard } from '../producto-card/producto-card';
import { CatalogoEstadoService } from '../services/catalogo-estado.service';
import { CategoriaService } from '../services/categoria.service';
import { ProductoService } from '../services/producto.service';

@Component({
  imports: [RouterLink, ProductoCard],
  selector: 'app-categorias',
  styleUrl: './categorias.css',
  templateUrl: './categorias.html',
})
export class Categorias {
  private readonly categoriaService = inject(CategoriaService);
  private readonly productoService = inject(ProductoService);
  private readonly catalogoEstado = inject(CatalogoEstadoService);
  private readonly router = inject(Router);

  categorias = this.catalogoEstado.categorias;
  productos = this.catalogoEstado.productos;
  categoriaSeleccionada = signal<Categoria | null>(null);
  cargando = signal(true);
  error = signal('');
  errorProductos = signal('');

  productosDeCategoria = computed(() => {
    const seleccionada = this.categoriaSeleccionada();
    if (!seleccionada) {
      return [];
    }
    return this.productos().filter((producto) => producto.categoriaId === seleccionada.id);
  });

  constructor() {
    this.categoriaService.obtenerCategorias().subscribe({
      next: (categorias) => {
        this.catalogoEstado.establecerCategorias(categorias);
        this.cargando.set(false);
      },
      error: () => {
        this.error.set('No se pudieron cargar las categorías.');
        this.cargando.set(false);
      },
    });

    this.productoService.obtenerProductos().subscribe({
      next: (productos) => this.catalogoEstado.establecerProductos(productos),
      error: () => this.errorProductos.set('No se pudieron cargar los productos de las categorías.'),
    });
  }

  seleccionarCategoria(categoria: Categoria): void {
    if (this.categoriaSeleccionada()?.id === categoria.id) {
      this.categoriaSeleccionada.set(null);
      return;
    }
    this.categoriaSeleccionada.set(categoria);
  }

  abrirDetalle(id: number): void {
    this.router.navigate(['/productos', id]);
  }
}
