import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { CartComponent } from "./cart/cart.component";
import { CheckoutComponent } from "./checkout/checkout.component";
import { FooterComponent } from "./shared/footer/footer.component";
import { HeaderComponent } from "./shared/header/header.component";
import { LoginComponent } from "./shared/login/login.component";
import { RegisterComponent } from "./shared/register/register.component";
import { AboutComponent } from "./about/about.component";
import { ProductDetailComponent } from "./product-detail/product-detail.component";
import { CategoryDetailComponent } from "./category-detail/category-detail.component";
import { AdminModule } from "./admin/admin.module";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CartComponent,
    CheckoutComponent,
    FooterComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,

    AboutComponent,
    ProductDetailComponent,
    CategoryDetailComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    AdminModule,
    RouterModule.forRoot([
      // Anonymous User
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "about", component: AboutComponent },
      { path: "login", component: LoginComponent },
      { path: "signup", component: RegisterComponent },
      { path: "product-detail", component: ProductDetailComponent },

      // Access for Registered Users
      { path: "shopping-cart", component: CartComponent },
      { path: "check-out", component: CheckoutComponent },


      // Admin Router
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
