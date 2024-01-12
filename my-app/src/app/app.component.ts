import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Observable, Subject, fromEvent } from 'rxjs';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'my-app';
  data$ = new Subject<string>()
  val = ''
  
  ngOnInit(){
    this.data$.asObservable().subscribe((value) => {
      this.val = value;
    })
  }
  change(event:Event){
    const inputValue = (event.target as HTMLInputElement).value;
    this.data$.next(inputValue);
  }
}
