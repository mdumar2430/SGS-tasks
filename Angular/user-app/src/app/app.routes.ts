import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UserListComponent } from './components/user-list/user-list.component';

export const routes: Routes = [
    {
        path:'',
        component:LoginComponent
    },
    {
        path:'users',
        component:UserListComponent,
    }
];
