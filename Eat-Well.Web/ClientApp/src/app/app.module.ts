import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { ProductsComponent } from "./products/products.component";
import { CartComponent } from "./cart/cart.component";
import { CheckoutComponent } from "./checkout/checkout.component";
import { FooterComponent } from "./shared/footer/footer.component";
import { HeaderComponent } from "./shared/header/header.component";
import { LoginComponent } from "./shared/login/login.component";
import { RegisterComponent } from "./shared/register/register.component";
import { AdminProductsComponent } from "./admin/admin-products/admin-products.component";
import { AdminOrdersComponent } from "./admin/admin-orders/admin-orders.component";
import { AdminUsersComponent } from "./admin/admin-users/admin-users.component";
import { AboutComponent } from "./about/about.component";
import { ProductDetailComponent } from "./product-detail/product-detail.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductsComponent,
    CartComponent,
    CheckoutComponent,
    FooterComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    AdminProductsComponent,
    AdminOrdersComponent,
    AdminUsersComponent,
    AboutComponent,
    ProductDetailComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      // Anonymous User
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "products", component: ProductsComponent },
      { path: "about", component: AboutComponent },
      { path: "login", component: LoginComponent },
      { path: "signup", component: RegisterComponent },

      // Access for Registered Users
      { path: "shopping-cart", component: CartComponent },
      { path: "check-out", component: CheckoutComponent },

      // Admin Router
      { path: "admin/orders", component: AdminOrdersComponent },
      { path: "admin/users", component: AdminUsersComponent },
      { path: "admin/products", component: AdminProductsComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
