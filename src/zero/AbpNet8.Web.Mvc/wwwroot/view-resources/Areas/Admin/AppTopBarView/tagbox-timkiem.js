function initTagBoxTimKiem(selector, staticSource) {
    let tagData = {};
    let suppress = false;

    const combo = $(selector).dxTagBox({
        dataSource: [], // 🔹 ban đầu trống
        searchEnabled: true,
        hideSelectedItems: true,
        placeholder: "Nhập từ khóa để tìm...",
        valueExpr: "key",
        displayExpr: "label", // hiển thị khi đang chọn trong danh sách
        onInput(e) {
            const text = e.event.target.value?.trim();
            if (!text) {
                combo.option("dataSource", []);
                return;
            }

            // 🔹 Tạo danh sách động (hiển thị đầy đủ khi chọn)
            const dynamic = staticSource.map(x => {
                const cleanLabel = x.label.replace(/^Tìm kiếm theo\s*/i, "");
                return {
                    key: `${x.key}@@${text}`,
                    label: `${x.label}: ${text}`, // hiển thị đầy đủ trong danh sách
                    shortLabel: `${cleanLabel}: ${text}` // rút gọn để hiển thị khi chọn
                };
            });

            combo.option("dataSource", dynamic);
        },
        onValueChanged(e) {
            if (suppress) return;
            const last = e.value.slice(-1)[0];
            if (!last) return;

            const ds = combo.option("dataSource");
            const selected = ds.find(x => x.key === last);
            if (!selected) return;

            const [type, val] = last.split("@@");
            if (!tagData[type]) tagData[type] = [];
            if (!tagData[type].includes(val)) tagData[type].push(val);

            // 🔹 Giữ nguyên label đầy đủ để xử lý, nhưng hiển thị rút gọn
            const merged = Object.keys(tagData).map(t => {
                const found = staticSource.find(s => s.key == t);
                const cleanLabel = found?.label.replace(/^Tìm kiếm theo\s*/i, "");
                return {
                    key: `${t}@@${tagData[t].join("!!")}`,
                    label: `${found?.label}: ${tagData[t].join(" hoặc ")}`,
                    shortLabel: `${cleanLabel}: ${tagData[t].join(" hoặc ")}`
                };
            });

            suppress = true;
            e.component.option({
                value: merged.map(x => x.key),
                dataSource: merged
            });
            suppress = false;
        },

        tagTemplate(item, el) {
            // 🔹 Nếu có shortLabel thì hiển thị rút gọn, không thì tự tạo từ label
            const text = item.shortLabel || item.label.replace(/^Tìm kiếm theo\s*/i, "");
            $("<div>")
                .addClass("custom-tag")
                .append(
                    $("<span>").addClass("tag-text").text(text),
                    $("<span>")
                        .addClass("tag-remove")
                        .text("✖")
                        .on("click", function () {
                            const [type] = item.key.split("@@");
                            delete tagData[type];

                            const merged = Object.keys(tagData).map(t => {
                                const found = staticSource.find(s => s.key == t);
                                const cleanLabel = found?.label.replace(/^Tìm kiếm theo\s*/i, "");
                                return {
                                    key: `${t}@@${tagData[t].join("!!")}`,
                                    label: `${found?.label}: ${tagData[t].join(" hoặc ")}`,
                                    shortLabel: `${cleanLabel}: ${tagData[t].join(" hoặc ")}`
                                };
                            });

                            suppress = true;
                            const inst = $(selector).dxTagBox("instance");
                            inst.option({
                                value: merged.map(x => x.key),
                                dataSource: merged.length ? merged : []
                            });
                            suppress = false;
                        })
                )
                .appendTo(el);
        }
    }).dxTagBox("instance");
}
