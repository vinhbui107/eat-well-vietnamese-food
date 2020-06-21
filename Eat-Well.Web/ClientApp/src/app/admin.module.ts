import { NgModule } from "@angular/core";
import { AdminComponent } from "./admin/admin.component";
import { Routes, RouterModule } from "@angular/router";
import { UserAdminComponent } from './user-admin/user-admin.component';
import { ProductAdminComponent } from './product-admin/product-admin.component';

const routes: Routes = [
  {
    path: "admin",
    component: AdminComponent,
    children: [],
  },
];

@NgModule({
  declarations: [AdminComponent, UserAdminComponent, ProductAdminComponent],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminModule {}
