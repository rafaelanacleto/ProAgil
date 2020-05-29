import { Eventos } from './Eventos';

export interface Lote {
    id: number; 
    nome: string; 
    preco: number; 
    dataIncio?: Date; 
    dataFim?: Date; 
    quantidade: number;
    eventoId: number;
    evento: Eventos;
    
}