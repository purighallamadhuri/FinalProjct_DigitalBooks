import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorCompComponent } from './author-comp.component';

describe('AuthorCompComponent', () => {
  let component: AuthorCompComponent;
  let fixture: ComponentFixture<AuthorCompComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorCompComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthorCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
