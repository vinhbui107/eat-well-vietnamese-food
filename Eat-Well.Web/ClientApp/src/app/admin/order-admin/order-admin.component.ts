import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";
declare var $: any;
@Component({
  selector: "app-order-admin",
  templateUrl: "./order-admin.component.html",
  styleUrls: ["./order-admin.component.css"],
})
export class OrderAdminComponent implements OnInit {
  Orders: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };

  Order: any = {
    id: Number,
    user_id: Number,
    order_total: Number,
    phone: String,
    date: Date,
    is_completed: Boolean,
    shipping_address: String,
    description: String,
  };
  isEdit: boolean = true;

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.GetAllOrder(1);
  }

  GetAllOrder(cPage) {
    let params = new HttpParams().set("page", cPage).set("size", "10");
    this.http.get("https://localhost:44317/api/Orders", { params }).subscribe(
      (result) => {
        this.Orders = result;
        this.Orders = this.Orders.data;
      },
      (error) => console.error(error)
    );
  }

  Next() {
    if (this.Orders.page < this.Orders.total_page) {
      let nextPage = this.Orders.page + 1;
      let params = new HttpParams().set("page", nextPage).set("size", "10");
      this.http.get("https://localhost:44317/api/Orders", { params }).subscribe(
        (result) => {
          this.Orders = result;
          this.Orders = this.Orders.data;
        },
        (error) => console.error(error)
      );
    } else {
      alert("Bạn đang ở trang cuối!");
    }
  }

  Previous() {
    if (this.Orders.page > 1) {
      let PrePage = this.Orders.page - 1;
      let params = new HttpParams()
        .set("page", PrePage.toString())
        .set("size", "10");
      this.http.get("https://localhost:44317/api/Orders", { params }).subscribe(
        (result) => {
          this.Orders = result;
          this.Orders = this.Orders.data;
        },
        (error) => console.error(error)
      );
    } else {
      alert("Bạn đang ở trang đầu!");
    }
  }

  //Modal
  openModal(isNew, index) {
    if (isNew) {
      this.isEdit = false;
      this.Order = {
        id: "",
        user_id: "",
        order_total: "",
        phone: "",
        date: "",
        is_completed: "",
        shipping_address: "",
        description: "",
      };
    } else {
      this.isEdit = true;
      this.Order = index;
    }
    $("#exampleModal").modal("show");
  }

  addOrder() {
    var x = {
      user_id: Number(this.Order.user_id),
      order_total: Number(this.Order.order_total),
      phone: String(this.Order.phone),
      date: this.Order.date,
      is_completed: Boolean(this.Order.is_completed),
      shipping_address: String(this.Order.shipping_address),
      description: String(this.Order.description),
    };
    console.log(x);
    this.http.post("https://localhost:44317/api/Orders", x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.Order = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  saveOrder(Id) {
    //   this.Product.options.forEach( (op) => {
    //     op.id = op.id.substring(0,10);
    // });

    var x = {
      id: Number(this.Order.id),
      user_id: Number(this.Order.user_id),
      order_total: Number(this.Order.order_total),
      phone: String(this.Order.phone),
      date: this.Order.date,
      is_completed: this.Order.is_completed,
      shipping_address: String(this.Order.shipping_address),
      description: this.Order.description,
    };
    console.log(x);
    this.http.put("https://localhost:44317/api/Orders/" + Id, x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.Order = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  deleteOrder(Id) {
    this.http.delete("https://localhost:44317/api/Orders/" + Id).subscribe(
      (result) => {
        alert("Xóa thành công!");
        this.Order = result;
        this.deleteOrder(Id);
        window.location.reload();
      },
      (error) => console.error(error)
    );
  }
}
