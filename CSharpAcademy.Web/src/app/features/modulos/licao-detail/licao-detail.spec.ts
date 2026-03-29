import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicaoDetail } from './licao-detail';

describe('LicaoDetail', () => {
  let component: LicaoDetail;
  let fixture: ComponentFixture<LicaoDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LicaoDetail],
    }).compileComponents();

    fixture = TestBed.createComponent(LicaoDetail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
