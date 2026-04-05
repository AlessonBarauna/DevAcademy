import { Injectable } from '@angular/core';
import confetti from 'canvas-confetti';

@Injectable({ providedIn: 'root' })
export class AnimacaoService {

  dispararConfetti(): void {
    confetti({
      particleCount: 120,
      spread: 80,
      origin: { y: 0.6 },
      colors: ['#00c896', '#7ec8ff', '#ffd700', '#ff6b6b', '#a29bfe']
    });
  }

  dispararConfettiModulo(): void {
    const end = Date.now() + 1500;
    const frame = () => {
      confetti({ particleCount: 6, angle: 60, spread: 55, origin: { x: 0 }, colors: ['#00c896', '#ffd700'] });
      confetti({ particleCount: 6, angle: 120, spread: 55, origin: { x: 1 }, colors: ['#7ec8ff', '#ff6b6b'] });
      if (Date.now() < end) requestAnimationFrame(frame);
    };
    frame();
  }
}
