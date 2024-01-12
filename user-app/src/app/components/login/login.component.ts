import { Component } from "@angular/core";
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import * as forge from 'node-forge';
import { Router } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { UserService } from "../../services/user.service";
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon'
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers:[HttpClientModule]
})
export class LoginComponent {
    userEmail: string = "";
    userPass: string = "";
    btnClicked: boolean = false;
    loginSuccess: boolean = false;

    publicKey: string = `-----BEGIN PUBLIC KEY-----
    MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAskgPKBcNpz71mi4NSYa5
    mazJrO0WZim7T2yy7qPxk2NqQE7OmWWakLJcaeUYnI0kO3yC57vck66RPCjKxWuW
    SGZ7dHXe0bWb5IXjcT4mNdnUIalR+lV8czsoH/wDUvkQdG1SJ+IxzW64WvoaCRZ+
    /4wBF2cSUh9oLwGEXiodUJ9oJXFZVPKGCEjPcBI0vC2ADBRmVQ1sKsZg8zbHN+gu
    U9rPLFzN4YNrCnEsSezVw/W1FKVS8J/Xx4HSSg7AyVwniz8eHi0e3a8VzFg+H09I
    5wK+w39sjDYfAdnJUkr6PjtSbN4/Sg/NMkKB2Ngn8oj7LCfe/7RNqIdiS+dQuSFg
    eQIDAQAB
    -----END PUBLIC KEY-----`;
    constructor(private _router: Router, private userService: UserService) { }

    login() {
      var rsa = forge.pki.publicKeyFromPem(this.publicKey);
      var encryptedPassword = window.btoa(rsa.encrypt(this.userPass));
      var payload = { "Email": this.userEmail, "Password": encryptedPassword };
      this.userService.userLogin(payload)
      .subscribe(
      {
          next: (res)=>{
            if(res){
              alert("Login SuccessFull!")
              this._router.navigate(['/users'])
            }
            else{
              alert("Invalid Password!")
            }
          },
          error: (err)=>{
            alert(err.error)
        }
      }
      )
    }

    logout() {
        this.loginSuccess = false;
        this.btnClicked = false;
    }
}
