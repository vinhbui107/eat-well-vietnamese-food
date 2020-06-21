import { Component, OnInit, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-product-detail",
  templateUrl: "./product-detail.component.html",
  styleUrls: ["./product-detail.component.css"],
})
export class ProductDetailComponent implements OnInit {
  Product: any = {
    id: Number,
    name: String,
    category: [],
    photo: String,
    description: String,
    slug: String,
    is_active: true,
    options: [],
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private _Activatedroute: ActivatedRoute,
    @Inject("BASE_URL") baseUrl: string
  ) {}

  ngOnInit() {
    const id = this._Activatedroute.snapshot.paramMap.get("id");
    console.log(id);
    this.getproductbyid(id);
  }

  getproductbyid(pro_id) {
    this.http.get("https://localhost:44317/api/Products/" + pro_id).subscribe(
      (result) => {
        this.Product = result;
        this.Product = this.Product.data[0];
        console.log(this.Product);
      },
      (error) => console.error(error)
    );
  }
}
