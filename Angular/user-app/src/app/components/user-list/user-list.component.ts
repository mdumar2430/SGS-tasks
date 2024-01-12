import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from '../../user';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css',
  imports:[CommonModule, RouterLink],
  providers: [HttpClientModule]
})
export class UserListComponent {
  users:User[]= []

  constructor(private userService: UserService){
  }

  ngOnInit(){
    this.userService.getUsers()
    .subscribe({
      next : (response) => {
        this.users = response
      }
    }
    )
  }
}
