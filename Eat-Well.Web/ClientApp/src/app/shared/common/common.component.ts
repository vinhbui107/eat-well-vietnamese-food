import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-common',
  templateUrl: './common.component.html',
  styleUrls: ['./common.component.css']
})
export class CommonComponent implements OnInit {

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
