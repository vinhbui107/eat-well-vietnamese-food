import { Component, OnInit } from "@angular/core";
import { add, total, list, remove, update, quantity } from "cart-localstorage";

@Component({
  selector: "app-cart",
  templateUrl: "./cart.component.html",
  styleUrls: ["./cart.component.css"],
})
export class CartComponent implements OnInit {
  cart: any = {
    products: [],
    total: Number,
  };

  ProductCart: any = {
    id: Number,
    name: String,
    quantity: Number,
    price: Number,
  };

  constructor() {}

  ngOnInit() {
    this.viewCart();
    this.cart.total = total();
  }

  viewCart() {
    this.cart.products = list();
    this.cart.total = total();
  }

  incrCart(id) {
    quantity(id, 1);
    window.location.reload();
  }
  dsCart(id) {
    quantity(id, -1);
    window.location.reload();
  }
  deleteProduct(id) {
    remove(id);
    window.location.reload();
  }
}
