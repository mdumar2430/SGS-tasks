import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { UserService } from '../../services/user.service';
import {MatListModule} from '@angular/material/list';
@Component({
  selector: 'sidenavbar',
  standalone: true,
  imports: [RouterOutlet, MatSidenavModule, MatListModule],
  templateUrl: './side-nav-bar.component.html',
  styleUrl: './side-nav-bar.component.css'
})
export class SideNavBarComponent {
  
  constructor(public userService:UserService){

  }
}
