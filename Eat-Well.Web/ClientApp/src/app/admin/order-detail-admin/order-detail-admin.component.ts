import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Router } from "@angular/router";
declare var $: any;

@Component({
  selector: "app-order-detail-admin",
  templateUrl: "./order-detail-admin.component.html",
  styleUrls: ["./order-detail-admin.component.css"],
})
export class OrderDetailAdminComponent implements OnInit {
  OrderDetails: any = {
    data: [],
    total_record: Number,
    page: Number,
    size: Number,
    total_page: Number,
  };

  OrderDetail: any = {
    product_id: Number,
    order_id: Number,
    price: Number,
    quantity: Number,
  };

  isEdit: boolean = true;

  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    this.GetOrderdetails(1);
    // this.getuseradminbyid(id);
  }

  GetOrderdetails(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page", cPage).set("size", "8");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http
      .get("https://localhost:44317/api/OrderDetails", { params })
      .subscribe(
        (result) => {
          this.OrderDetails = result;
          this.OrderDetails = this.OrderDetails.data;
        },
        (error) => console.error(error)
      );
  }

  Next() {
    if (this.OrderDetails.page < this.OrderDetails.total_page) {
      let nextPage = this.OrderDetails.page + 1;
      let params = new HttpParams().set("page", nextPage).set("size", "10");
      this.http
        .get("https://localhost:44317/api/OrderDetails", { params })
        .subscribe(
          (result) => {
            this.OrderDetails = result;
            this.OrderDetails = this.OrderDetails.data;
          },
          (error) => console.error(error)
        );
    } else {
      alert("Bạn đang ở trang cuối!");
    }
  }

  Previous() {
    if (this.OrderDetails.page > 1) {
      let PrePage = this.OrderDetails.page - 1;
      let params = new HttpParams()
        .set("page", PrePage.toString())
        .set("size", "10");
      this.http
        .get("https://localhost:44317/api/OrderDetails", { params })
        .subscribe(
          (result) => {
            this.OrderDetails = result;
            this.OrderDetails = this.OrderDetails.data;
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
      this.OrderDetail = {
        product_id: "",
        order_id: "",
        price: "",
        quantity: "",
      };
    } else {
      this.isEdit = true;
      this.OrderDetail = index;
    }
    $("#exampleModal").modal("show");
  }

  addOrderDetails() {
    var x = {
      product_id: Number(this.OrderDetail.product_id),
      order_id: Number(this.OrderDetail.order_id),
      price: Number(this.OrderDetail.price),
      quantity: Number(this.OrderDetail.quantity),
    };
    console.log(x);
    this.http.post("https://localhost:44317/api/OrderDetails", x).subscribe(
      (result) => {
        var res: any = result;
        if (res.success) {
          this.OrderDetail = res.data;
          window.location.reload();
        }
      },
      (error) => console.error(error)
    );
  }

  saveOrderDetails(Id) {
    var x = {
      product_id: Number(this.OrderDetail.product_id),
      order_id: Number(this.OrderDetail.order_id),
      price: Number(this.OrderDetail.price),
      quantity: Number(this.OrderDetail.quantity),
    };
    this.http
      .put("https://localhost:44317/api/OrderDetails/" + Id, x)
      .subscribe(
        (result) => {
          var res: any = result;
          if (res.success) {
            this.OrderDetail = res.data;
            window.location.reload();
          }
        },
        (error) => console.error(error)
      );
  }

  deleteOrderDetail(OrdId: Number, ProId: Number) {
    let params = new HttpParams()
      .set("ordId", OrdId.toString())
      .set("proId", ProId.toString());
    this.http
      .delete("https://localhost:44317/api/OrderDetails", { params })
      .subscribe(
        (result) => {
          alert("Xóa thành công!");
          this.OrderDetail = result;
          this.deleteOrderDetail(OrdId, ProId);
          window.location.reload();
        },
        (error) => console.error(error)
      );
  }
}
