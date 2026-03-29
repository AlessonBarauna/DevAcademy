import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuloList } from './modulo-list';

describe('ModuloList', () => {
  let component: ModuloList;
  let fixture: ComponentFixture<ModuloList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModuloList],
    }).compileComponents();

    fixture = TestBed.createComponent(ModuloList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
