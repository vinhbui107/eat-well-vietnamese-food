import { Router } from "@angular/router";
import { Component, OnInit, Inject } from "@angular/core";
import { add, total, list, remove, update, quantity } from "cart-localstorage";
import { HttpClient } from "@angular/common/http";

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

  UserInforCheckout: any = {
    phone: "",
    address: "",
    description: "",
  };

  order: any = {
    id: Number,
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

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

  checkout() {
    let customerId = window.localStorage.getItem("customerId");
    if (customerId == null) {
      this.router.navigate(["login"]);
      alert("Bạn cần đăng nhập trước khi thanh toán!");
    } else {
      if (list().length === 0) {
        alert("Giỏ hàng của bạn đang trống!");
      } else {
        // tạo hàng dữ liệu order
        this.createOrder();
        // tạo hàng dữ liệu ở bảng order detail
        for (var od of list()) {
          this.createOrderDetail(od);
        }
        window.localStorage.removeItem("__cart");
        alert(
          "Bạn đã thanh toán thành công. Hãy đợi chúng tôi liên lạc xác nhận nhé!"
        );
        this.router.navigate(["/"]);
      }
    }
  }

  createOrder() {
    var order = {
      user_id: Number(window.localStorage.getItem("customerId")),
      order_total: Number(total()),
      phone: this.UserInforCheckout.phone,
      date: "2020-06-23T13:37:29.739Z",
      is_completed: false,
      shipping_address: this.UserInforCheckout.address,
      description: this.UserInforCheckout.description,
    };
    console.log(order);
    this.http.post("https://localhost:44317/api/Orders", order).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          console.log("Create order work.");
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  createOrderDetail(od) {
    var orderDetail = {
      product_id: Number(od.id),
      order_id: Number(od.id),
      price: Number(od.price),
      quantity: Number(od.quantity),
    };
    this.http
      .post("https://localhost:44317/api/OrderDetails", orderDetail)
      .subscribe(
        (result) => {
          var res: any = result;
          if (res.success) {
            this.order.id = res.data.orderId;
            console.log("Create order work.");
          }
        },
        (error) => {
          console.error(error);
        }
      );
  }
}
