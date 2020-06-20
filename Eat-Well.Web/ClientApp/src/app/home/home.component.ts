import { Component, Inject } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  Products: any = {
    data: [],
    total_record:0,
    page: 4,
    size:8,
    total_page:0
  };


  constructor(
    private http: HttpClient, private router:Router,
    @Inject("BASE_URL") baseUrl: string) {}

  ngOnInit() {
    this.GetAllProductWithPagination(4);
  }

  GetAllProductWithPagination(cPage) {
    //Tạo mới 2 parameter, page và size.
    let params = new HttpParams().set("page",cPage).set("size", "8");
    //Gọi Get method truyền vào 2 parameter.
    //Trả về danh sách Product với page = 1 và size = 10.
    this.http.get("https://localhost:44317/api/Products", {params}).subscribe(
      result => {
        this.Products = result;
        this.Products = this.Products.data;
      },error => console.error(error)
    );
}
}
