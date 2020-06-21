import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-category-detail",
  templateUrl: "./category-detail.component.html",
  styleUrls: ["../home/home.component.css"],
})
export class CategoryDetailComponent implements OnInit {
  Category: any = {
    id: Number,
    name: String,
    slug: String,
    products: [],
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
    this.getbyid(id);
  }

  getbyid(cate_id) {
    this.http
      .get("https://localhost:44317/api/Categories/" + cate_id)
      .subscribe(
        (result) => {
          this.Category = result;
          this.Category = this.Category.data[0];
        },
        (error) => console.error(error)
      );
  }

  //Get Id Product
  getIdProduct(pro_id) {
    this.http.get("https://localhost:44317/api/Products/" + pro_id).subscribe(
      (result) => {
        this.router.navigate(["/product/" + pro_id]);
      },
      (error) => console.error(error)
    );
  }
}
