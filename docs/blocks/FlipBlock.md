# Flip Block

## Description
Block shall flip images horizontally or vertically.

---

## Configuration Parameters

### `FlipMode`
Specifies the direction of the flip.
- **Horizontal**: Mirrors the image along the vertical axis (left becomes right).
- **Vertical**: Mirrors the image along the horizontal axis (top becomes bottom).

---

## Acceptance Criteria
- Output image is flipped according to the `FlipMode`.
- Image dimensions and format are preserved.

---

## UI Behaviour
- **FlipMode** dropdown allows selecting "Horizontal" or "Vertical".

---

## Operational Behaviour

### Execution
- Applies `Image.Mutate(x => x.Flip(...))` using `FlipMode.Horizontal` or `FlipMode.Vertical`.
