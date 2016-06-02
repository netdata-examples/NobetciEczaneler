<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eczaneler.aspx.cs" Inherits="eczaneler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="title" runat="server">Nöbetçi Eczaneler</title>
    <meta id="description" runat="server" name="description" content="Günlük olarak tüm il ve ilçelerin nöbetçi eczanelerini gösteren bir web sitesi." />
    <meta id="keywords" runat="server" name="keywords" content="nöbetçi eczaneler, il nöbetçi eczaneleri, ilçe nöbetçi eczaneleri" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <meta charset="utf-8">

    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/bootstrap-select.min.css" rel="stylesheet" />
    <link href="/css/loader.css" rel="stylesheet" />
    <link href="/css/sitil.css" rel="stylesheet" />

    <script type="text/javascript" charset="windows-1254" src="/js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/js/bootstrap.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/js/bootbox.min.js"></script>
    <script type="text/javascript" charset="windows-1254" src="/js/main.js"></script>
    <script src="JS/Sharer.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".loaderStore").fadeOut("slow");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loaderStore">
            <div class='uil-reload-css'>
                <div></div>
            </div>
            <div class="divLoaderMesaj">
                <span class="spnLoaderMesajMetin"></span>
            </div>
        </div>

        <nav style="background: #4285F4; border-color: #1995dc;" class="navbar  navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a style="padding-top: 10px;" class="navbar-brand" href="http://www.netdata.com/">
                        <img src="/Img/logofornetsite2.png" alt="Netdata">
                    </a>
                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav">
                        <li class=""><a target="_blank" href="http://www.netdata.com/project/0b01ba62/nobetci-eczaneler"><span class="spnShowDatas">Projeyi Satın Al</span></a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li style="height: 32px;">
                            <div style="margin-top: 8px;" class="netdata-social-share text-center"></div>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container-fluid form-group">
            <div class="row">
                <div class="col-xs-12 col-sm-9">
                    <div class="row form-group">
                        <div class="col-xs-12 col-sm-4">
                            <div class="row text-center">
                                <img src="/Img/NobetciEczane.png" alt="." class="img-thumbnail img-responsive imgEczane" />
                            </div>

                        </div>
                        <div class="col-xs-12 col-sm-8">
                            <div class="row form-group">
                                <div class="col-xs-12 text-center">
                                    <h1 class="text-danger">NÖBETÇİ ECZANELER</h1>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 text-center">
                                    <h2 id="tarihBaslik" runat="server"></h2>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 text-center">
                                    <h2 id="sehirBaslik" runat="server"></h2>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-4">
                            <div class="panel panel-danger">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-12 form-group">
                                            <select id="txtIl" class="selectpicker" data-live-search="true">
                                            </select>
                                            <asp:HiddenField ID="hfSehir" runat="server" />
                                        </div>
                                        <div class="col-xs-12">
                                            <select id="txtIlce" class="selectpicker" data-live-search="true">
                                                <option value="0">İlçe Seçiniz...</option>
                                            </select>
                                            <asp:HiddenField ID="hfIlce" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer text-center">
                                    <button type="button" class="btn btn-danger" onclick="EczaneAra()">
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        <span>ARA</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <span style="line-height: 19px; font-size: 13px;" class="text-danger">*Bu proje verileri Netdata'ya ait değildir. Veriler proje sahibi tarafından girilip, güncellenmektedir </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-8">
                            <div id="eczanelerList" runat="server" class="row text-center"></div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-3 hidden-xs">
                    <div class="panel panel-danger">
                        <div id="sagTarafIller" class="panel-body"></div>
                    </div>
                </div>
            </div>
        </div>




    </form>
</body>
</html>
