
//Importa o objeto componente, para poder chama-lo abaixo
import { Component } from '@angular/core';

//Aqui ele define o nome do seletor, a pagina html e o estilo CSS
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//Aqui, criamos objetos para serem usados no HTML. No exemplo abaixo, ele exporta um objeto "title".
export class AppComponent {
  title = 'ProAgil Eventos';
}
