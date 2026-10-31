// Alleen wat het scherm zelf doet. Alles wat opgeslagen moet worden, gaat met een formulier naar de server.

document.querySelectorAll("[data-order-screen]").forEach(screen => {
  const buttons = screen.querySelectorAll("[data-category]");
  const panes = screen.querySelectorAll("[data-category-pane]");
  const title = screen.querySelector("[data-category-title]");
  const search = screen.querySelector("[data-product-search]");

  function showCategory(category) {
    buttons.forEach(b => b.classList.toggle("actief", b.dataset.category === category));
    panes.forEach(p => p.hidden = p.dataset.categoryPane !== category);
    title.textContent = category;
    search.value = "";
    screen.querySelectorAll("[data-row]").forEach(row => row.hidden = false);
  }

  buttons.forEach(b => b.addEventListener("click", () => showCategory(b.dataset.category)));

  search.addEventListener("input", () => {
    const text = search.value.trim().toLowerCase();
    if (text === "") {
      const active = screen.querySelector("[data-category].actief");
      if (active) showCategory(active.dataset.category);
      return;
    }
    title.textContent = "Zoekresultaat";
    panes.forEach(p => p.hidden = false);
    screen.querySelectorAll("[data-row]").forEach(row => row.hidden = !row.textContent.toLowerCase().includes(text));
  });

  if (buttons.length > 0) showCategory(buttons[0].dataset.category);
});

document.querySelectorAll("[data-search]").forEach(input => {
  input.addEventListener("input", () => {
    const text = input.value.trim().toLowerCase();
    document.querySelectorAll(input.dataset.search).forEach(row => row.hidden = !row.textContent.toLowerCase().includes(text));
  });
});

// R-07: het aantal is 1 tot en met 99. De server moet dit ook controleren.
document.querySelectorAll("[data-stepper]").forEach(stepper => {
  const shown = stepper.querySelector("b");
  const value = stepper.querySelector("input");
  stepper.querySelectorAll("[data-delta]").forEach(button => button.addEventListener("click", () => {
    const quantity = Math.min(99, Math.max(1, Number(value.value) + Number(button.dataset.delta)));
    value.value = quantity;
    shown.textContent = quantity;
  }));
});

document.querySelectorAll("[data-counter]").forEach(button => button.addEventListener("click", () => {
  const value = button.querySelector("input");
  const count = (Number(value.value) + 1) % 4;
  value.value = count;
  button.querySelector(".teller").textContent = count;
  button.classList.toggle("aan", count > 0);
}));

document.querySelectorAll("[data-counter]").forEach(button =>
  button.classList.toggle("aan", Number(button.querySelector("input").value) > 0));

document.querySelectorAll("[data-tab]").forEach(tab => tab.addEventListener("click", () => {
  document.querySelectorAll("[data-tab]").forEach(t => t.classList.toggle("aan", t === tab));
  document.querySelectorAll("[data-tab-pane]").forEach(p => p.hidden = p.dataset.tabPane !== tab.dataset.tab);
}));

document.querySelectorAll("[data-print]").forEach(button => button.addEventListener("click", () => window.print()));

document.querySelectorAll("[data-new-form]").forEach(button => button.addEventListener("click", () => {
  const form = document.querySelector(`[data-form="${button.dataset.newForm}"]`);
  form.querySelectorAll("input:not([type=radio]):not([type=checkbox]):not([type=hidden])").forEach(input => input.value = "");
  form.querySelector("[data-form-title]").textContent = button.textContent;
  form.querySelector("[data-form-subtitle]").textContent = "nog niet opgeslagen";
  form.querySelector("input:not([type=radio]):not([type=checkbox]):not([type=hidden])").focus();
}));

const pinValue = document.querySelector("[data-pin-value]");
if (pinValue) {
  const dots = document.querySelectorAll("[data-pin-dots] i");
  const showDots = () => dots.forEach((dot, i) => dot.classList.toggle("vol", i < pinValue.value.length));

  document.querySelectorAll("[data-digit]").forEach(key => key.addEventListener("click", () => {
    pinValue.value += key.dataset.digit;
    showDots();
    if (pinValue.value.length === 4) pinValue.form.submit();
  }));

  document.querySelector("[data-pin-clear]").addEventListener("click", () => {
    pinValue.value = "";
    showDots();
  });

  document.querySelectorAll("[data-login-names] input").forEach(input => input.addEventListener("change", () => {
    document.querySelector("[data-login-name]").textContent = input.value;
    pinValue.value = "";
    showDots();
  }));
}
