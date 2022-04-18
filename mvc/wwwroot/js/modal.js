// В этом файле функции для работы с модальными окнами в админке сайта

        $(document).ready(function () { // зaпускaем скрипт пoсле зaгрузки всех элементoв

            // глобальные переменные для получения данных из модели и передачи в форму
            var id;
            var number;            
            var title;
            var slug;
            var sku;            
            var dimension;
            var price;
            var pricebig;
            var description;
            var summary;
            var materials;
            var imghome;
            //var imgnew;   
            var imgone;
            var imggal;
            var video;

            var imghomeid;
            //var imgnewid;
            var imgoneid;
            var imggalid;
            var videoid;

            var isnew;         // чекбокс приходит bool
            var ispublished;   // чекбокс приходит bool

            var imggalquan;    // количество фоток в галерее
            var categoryid;
            var url;

            //--- Modal Product Add New --------
            $("#addProd").on("click", function () {

                // делаем заголовок
                $("#headlabel").text("Создание нового изделия");

                // меняем action формы
                $('#prodEdit').attr('action', '/dash/prodnew');

                // показываю окно с формой после изменения параметров формы
                $('#editProduct').modal('show');
            });

            //--- Modal Product Edit --------
            $(".editProd").on("click", function () {

                // делаем заголовок
                $("#headlabel").text("Редактирование изделия");

                // меняем action формы
                $('#prodEdit').attr('action', '/dash/prodedit');

                // получаю данные редактируемого товара из кнопки
                id = $(this).data("id");

                imghomeid = $(this).data("imghomeid");
                //imgnewid = $(this).data("imgnewid");
                imgoneid = $(this).data("imgoneid");
                imggalid = $(this).data("imggalid");
                videoid = $(this).data("videoid");

                number = $(this).data("number");                
                title = $(this).data("title");
                sku = $(this).data("sku");                
                dimension = $(this).data("dimension");
                price = $(this).data("price");
                pricebig = $(this).data("pricebig");
                description = $(this).data("description");
                summary = $(this).data("summary");
                materials = $(this).data("materials");
                imghome = $(this).data("imghome");
                //imgnew = $(this).data("imgnew");
                imgone = $(this).data("imgone");
                imggal = $(this).data("imggal");
                video = $(this).data("video");

                ispublished = $(this).data("ispublished");      // чекбокс
                isnew = $(this).data("isnew");                  // чекбокс

                imggalquan = $(this).data("imggalquan");   // количество фоток в галерее
                categoryid = $(this).data("categoryid");   // ID категории

                // передаю данные в форму по ID формы и name инпутов
                //$("#id").val(id);
                $('#prodEdit [name="id"]').val(id);

                $('#prodEdit [name="imghomeid"]').val(imghomeid);
                //$('#prodEdit [name="imgnewid"]').val(imgnewid);
                $('#prodEdit [name="imgoneid"]').val(imgoneid);
                $('#prodEdit [name="imggalid"]').val(imggalid);
                $('#prodEdit [name="videoid"]').val(videoid);

                $('#prodEdit [name="number"]').val(number);
                $('#prodEdit [name="title"]').val(title);
                $('#prodEdit [name="sku"]').val(sku);
                $('#prodEdit [name="dimension"]').val(dimension);
                $('#prodEdit [name="price"]').val(price);
                $('#prodEdit [name="pricebig"]').val(pricebig);
                $('#prodEdit [name="description"]').val(description);
                $('#prodEdit [name="materials"]').val(materials);
                $('#prodEdit [name="summary"]').val(summary);
                $('#prodEdit [name="imghome"]').val(imghome);
                //$('#prodEdit [name="imgnew"]').val(imgnew);
                $('#prodEdit [name="imgone"]').val(imgone);
                $('#prodEdit [name="imggal"]').val(imggal);
                $('#prodEdit [name="video"]').val(video);

                // новинка в чекбокс по ID формы                
                if (isnew == "True") { $('#prodEdit [name="isnew"]').prop('checked', true); }
                else { $('#prodEdit [name="isnew').prop('checked', false); };

                // опубликовано в чекбокс  по ID формы
                if (ispublished == "True") { $('#prodEdit [name="ispublished"]').prop('checked', true); }
                else { $('#prodEdit [name="ispublished"]').prop('checked', false); };
                
                // selects
                $("#cat").val(categoryid).trigger("chosen:updated");        // категория
                $("#quansel").val(imggalquan).trigger("chosen:updated");    // кол-во картинок в галерее одной строаницы

                // показываю окно с формой после изменения параметров формы
                $('#editProduct').modal('show');

                console.log(imghomeid);
                console.info(imghomeid);

            });

            //--- Modal Product Delete Show Modal --------
            $(".delProd").on("click", function () {

                // получаю id редактируемого товара
                id = $(this).data("id");

                // показываю окно с формой после изменения параметров формы
                $('#delProduct').modal('show');
            });

            //--- Redirect Product Delete Submit Button ---
            $(".deleteProd").on("click", function () {
                url = "/dash/proddel/" + id;
                location.href = url;
            });

            //--- Modal Category Add New --------
            $("#addCat").on("click", function () {
                $('#addCategory').modal('show');
            });

            //--- Modal Category Edit --------
            $(".editCat").on("click", function () {

                // получаю данные редактируемой категории
                id = $(this).data("id");
                title = $(this).data("title");
                slug = $(this).data("slug");
                description = $(this).data("description");
                number = $(this).data("number");
                ispublished = $(this).data("ispublished");

                // передача данных в форму по ID формы
                $('#editCategory [name="id"]').val(id);
                $('#editCategory [name="title"]').val(title);
                $('#editCategory [name="slug"]').val(slug);
                $('#editCategory [name="description"]').val(description);
                $('#editCategory [name="number"]').val(number);

                // опубликовано в чекбокс по ID формы
                if (ispublished == "True") { $('#editCategory [name="ispublished"]').prop('checked', true); }
                else { $('#editCategory [name="ispublished"]').prop('checked', false); };

                // показываю окно с формой после изменения параметров формы
                $('#editCategory').modal('show');
                
            });

            //--- Modal Category Delete Show Modal --------
            $(".delCat").on("click", function () {

                // получаю id редактируемого товара
                id = $(this).data("id");

                // показываю окно с формой после изменения параметров формы
                $('#delCategory').modal('show');
            });

            //--- Redirect Category Delete Submit Button ---
            $(".deleteCat").on("click", function () {
                url = "/dash/catdel/" + id;
                location.href = url;
            });

            //--- Modal Video --------
            $(".btnvideo").on("click", function () {

                // получение данных из кнопки и передача в форму
                video = $(this).data("video");
                $("#iframe").attr("src", video);

                // показываю окно с формой после изменения параметров формы
                $('#video').modal('show');
            });

            // закрываем/прячем все модальные окна по классу
            $(".close").on("click", function () {
                $('#editCategory').modal('hide');
                $('#delCategory').modal('hide');
                $('#addCategory').modal('hide');
                $('#editProduct').modal('hide');
                $('#video').modal('hide');
            });
        });