@echo off
cd /d %~dp0

echo Content.mgcbを再作成します

setlocal

echo #----------------------------- Global Properties ----------------------------# > Content.mgcb

echo /outputDir:bin/$(Platform) >> Content.mgcb
echo /intermediateDir:obj/$(Platform) >> Content.mgcb
echo /platform:Windows >> Content.mgcb
echo /config: >> Content.mgcb
echo /profile:Reach >> Content.mgcb
echo /compress:False >> Content.mgcb

echo #-------------------------------- References --------------------------------# >> Content.mgcb


echo #---------------------------------- Content ---------------------------------# >> Content.mgcb

endlocal

echo 再作成が完了しました

exit /b 0