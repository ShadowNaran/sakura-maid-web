export interface Categoria {
  id: number;
  nombre: string;
  descripcion: string | null;
  cantidadProductos: number;
}

export type CategoriaRequest = Omit<Categoria, 'id' | 'cantidadProductos'>;
