import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members:Member[];
  pagination:Pagination;
  userParams:UserParams;

  genderList=[{value:"male",display:"Males"},{value:"female",display:"Females"}];

  constructor(private memberService: MembersService) { 
      this.userParams = memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers();
  }
  loadMembers(){
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe(response=>{
      this.pagination=response.pagination;
      this.members = response.result;
    })
  }
  pageChanged(event:any){
    this.memberService.setUserParams(this.userParams);
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }
  resetFilters(){
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
  }
}
