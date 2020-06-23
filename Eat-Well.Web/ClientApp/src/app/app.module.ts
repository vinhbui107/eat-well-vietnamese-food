import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";

import { AppComponent } from "./app.component";

import { SiteLayoutComponent } from "./_layout/site-layout/site-layout.component";
import { AdminLayoutComponent } from "./_layout/admin-layout/admin-layout.component";
import { SidebarComponent } from "./_layout/sidebar/sidebar.component";
import { HeaderComponent } from "./_layout/header/header.component";
import { FooterComponent } from "./_layout/footer/footer.component";

import { LoginComponent } from "./shared/login/login.component";
import { RegisterComponent } from "./shared/register/register.component";

import { HomeComponent } from "./home/home.component";
import { AboutComponent } from "./about/about.component";

import { CategoryDetailComponent } from "./category-detail/category-detail.component";

import { ProductDetailComponent } from "./product-detail/product-detail.component";

import { CartComponent } from "./cart/cart.component";
import { CheckoutComponent } from "./checkout/checkout.component";
import { OrderUserComponent } from "./order-user/order-user.component";

import { DashboardComponent } from "./admin/dashboard/dashboard.component";
import { UserAdminComponent } from "./admin/user-admin/user-admin.component";
import { ProductAdminComponent } from "./admin/product-admin/product-admin.component";
import { OrderAdminComponent } from "./admin/order-admin/order-admin.component";

import { routing } from "./app.routing";
import { HttpClientModule } from "@angular/common/http";
import { ProductOptionAdminComponent } from "./admin/product-option-admin/product-option-admin.component";
import { ProfileComponent } from "./profile/profile.component";

@NgModule({
  imports: [BrowserModule, FormsModule, routing, HttpClientModule],
  declarations: [
    AppComponent,

    FooterComponent,
    HeaderComponent,

    HomeComponent,
    AboutComponent,

    LoginComponent,
    RegisterComponent,
    ProfileComponent,

    CartComponent,
    CheckoutComponent,

    ProductDetailComponent,
    CategoryDetailComponent,

    ProductAdminComponent,
    UserAdminComponent,
    DashboardComponent,
    SidebarComponent,
    AdminLayoutComponent,
    SiteLayoutComponent,
    OrderUserComponent,
    OrderAdminComponent,
    ProductOptionAdminComponent,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
