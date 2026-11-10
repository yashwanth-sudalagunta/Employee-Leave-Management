const dialog = document.getElementById("requestDialog");
const form = document.getElementById("leaveForm");
const toast = document.getElementById("toast");
const rows = document.getElementById("requestRows");
const pendingCount = document.getElementById("pendingCount");

document.getElementById("openRequest").addEventListener("click", () => dialog.showModal());

function showToast(message) {
  toast.textContent = message;
  toast.classList.add("show");
  window.setTimeout(() => toast.classList.remove("show"), 2800);
}

function countBusinessDays(start, end) {
  let count = 0;
  const date = new Date(`${start}T12:00:00`);
  const last = new Date(`${end}T12:00:00`);
  while (date <= last) {
    if (date.getDay() !== 0 && date.getDay() !== 6) count++;
    date.setDate(date.getDate() + 1);
  }
  return count;
}

form.addEventListener("submit", (event) => {
  event.preventDefault();
  const type = document.getElementById("leaveType").value;
  const start = document.getElementById("startDate").value;
  const end = document.getElementById("endDate").value;
  const reason = document.getElementById("reason").value.trim();

  if (!start || !end || end < start || reason.length < 5) {
    showToast("Check the dates and enter a reason of at least 5 characters.");
    return;
  }

  const days = countBusinessDays(start, end);
  const format = (value) => new Date(`${value}T12:00:00`).toLocaleDateString("en-US", {month:"short", day:"2-digit"});
  const tr = document.createElement("tr");
  const td1 = document.createElement("td");
  const dot = document.createElement("span");
  dot.className = "leave-dot vacation";
  const strong = document.createElement("strong");
  strong.textContent = type;
  const small = document.createElement("small");
  small.textContent = reason;
  td1.append(dot, strong, small);

  const td2 = document.createElement("td");
  td2.textContent = `${format(start)} – ${format(end)}`;
  const td3 = document.createElement("td");
  td3.textContent = String(days);
  const td4 = document.createElement("td");
  const status = document.createElement("span");
  status.className = "status pending";
  status.textContent = "Pending";
  td4.append(status);
  tr.append(td1, td2, td3, td4);
  rows.prepend(tr);

  const current = Number.parseInt(pendingCount.textContent, 10) || 0;
  pendingCount.innerHTML = `${current + 1} <small>requests</small>`;
  form.reset();
  dialog.close();
  showToast("Demo request added. Connect the API to persist requests.");
});
